using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using DataAccessLayer.Model;
using DataAccessLayer.Model.Enum;

namespace DataAccessLayer
{
    public class StagingRepository_Dictionary
    {
        private string _connectionString = @"Data Source = JERUSALEM\sqlstg;Initial Catalog = staging1; Persist Security Info=True;User ID = usr_neogig; Password=r2kR9HD0XJ5Uyjsy6Xxweh3prodo4gzaP8IA79KdixqyP17H";

        public List<Dictionary<string, string>> GetRecords(List<FileConfiguration> configurations, int limitTop)
        {
            
            var stopWatch = Stopwatch.StartNew();
            var targetTable = configurations.First().ExtractTable;
            var result = new List<Dictionary<string, string>>();

            $"Getting records from {targetTable}".Write();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT TOP {limitTop} * FROM {targetTable}";
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        result = this.DataReaderMapToList(reader);
                    }
                }
            }
            stopWatch.Stop();
            $"Execution time: {stopWatch.Elapsed} | Returned {result.Count} lines".Write();
            Util.BlankLine();

            return result;
        }

        public List<Critic> ValidateRecords(List<Dictionary<string, string>> records, List<FileConfiguration> configurations)
        {
            var stopWatch = Stopwatch.StartNew();
            $"Initializing validation of {records.Count()} lines...".Write();
            var critics = new List<Critic>();

            var line = 1;

            foreach (var record in records)
            {
                foreach (var cfg in configurations)
                {
                    var value = record[cfg.ColumnName];
                    if(string.IsNullOrEmpty(value)) continue;

                    switch (cfg.TypeColumn)
                    {
                        case TypeEnum.Bigint:
                            if(!value.IsLong())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            
                            break;
                        case TypeEnum.Bit:
                            if (!value.IsConvertibleToBool())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));

                            break;
                        case TypeEnum.Char:
                            if (value.IsNullOrEmpty())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            break;
                        case TypeEnum.Date:
                            if (!value.IsDate())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            break;
                        case TypeEnum.Datetime:
                            if (!value.IsDate())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            break;
                        case TypeEnum.Decimal:
                            if (!value.IsFloat())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            break;
                        case TypeEnum.Float:
                            if (!value.IsFloat())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            break;
                        case TypeEnum.Int:
                            if (!value.IsInteger())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            break;
                        case TypeEnum.Numeric:
                            if (!value.IsInteger())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            break;
                        case TypeEnum.Smalldatetime:
                            if (!value.IsDate())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            break;
                        case TypeEnum.Text:
                            if (value.IsNullOrEmpty())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            break;
                        case TypeEnum.Time:
                            if (!value.IsDate())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            break;
                        case TypeEnum.Varchar:
                            if (value.IsNullOrEmpty())
                                critics.Add(new Critic(line, value, cfg.ColumnName, cfg.TypeColumn));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                line++;
            }

            stopWatch.Stop();
            $"Execution time: {stopWatch.Elapsed} | Returned {critics.Count} Critics {records.Count()} lines analysed.".Write();

            return critics;
        }

        public List<Dictionary<string, string>> DataReaderMapToList(IDataReader reader)
        {
            var result = new List<Dictionary<string, string>>();
            while (reader.Read())
            {
                var dict = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dict.Add(reader.GetName(i), reader.IsDBNull(i) ? null : reader.GetValue(i).ToString());
                }

                result.Add(dict);
            }

            return result;
        }

        public List<FileConfiguration> GetFileConfiguration(int idFile)
        {
            $"Getting configurations of File ID = {idFile}".Write();
            var stopWatch = Stopwatch.StartNew();
            var result = new List<FileConfiguration>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText =
                            "SELECT P.ID_CLIENTE, P.PROJETO,	A.IDARQUIVO,	A.ARQUIVO,	A.PLANILHA,	A.TABELA_EXTRACAO,	NM_COLUNA_EXT,	" +
                            " TM_DAD_DST_TRF,	USER_TYPE_ID " +
                            " FROM [dbo].[ETL_CONFIGURACAO_PROJETO] P " +
                            " INNER JOIN dbo.[ETL_CONFIGURACAO_ARQUIVO] A ON P.IDPROJETO = A.IDPROJETO" +
                            " INNER JOIN dbo.[ETL_ESTRUTURA_PLANILHA] E ON E.IDARQUIVO = A.IDARQUIVO " +
                            " WHERE E.NM_COLUNA<> 'LINHA' AND A.IDARQUIVO = @idFile";
                        cmd.Parameters.AddWithValue("@idFile", idFile);

                        connection.Open();
                        var reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            while (reader.Read())
                            {
                                result.Add(new FileConfiguration
                                {
                                    IdClient = reader.GetInt32(0),
                                    IdFile = reader.GetInt32(2),
                                    Project = reader.GetString(1),
                                    FileName = reader.GetString(3),
                                    Worksheet = reader.GetString(4),
                                    ExtractTable = reader.GetString(5),
                                    ColumnName = reader.GetString(6),
                                    TypeColumn = (TypeEnum)reader.GetInt32(8)
                                });
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                        
                    }
                }
            }
            stopWatch.Stop();
            $"Execution time: {stopWatch.Elapsed} | Returned {result.Count} lines".Write();
            Util.BlankLine();

            return result;
        }
    }
}
