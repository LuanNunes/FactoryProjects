console.log('Hello mothafucker!');

const params = {
    url: 'http://localhost/webapi/api/home/search/async/a',
    method: 'GET'
};


const get = () => {
    $.ajax({
        url: 'http://localhost/webapi/api/home/search/a',
        method: 'GET',
        dataType: "application/json",
        success: function(data){
            console.log(data);
        }
    });
}

const getAsync = () => {
    oboe(params).node('!', (json) => {
        console.log(json);
    }).node('eof', () =>{
        console.log('Finish');
    }).fail(() => {
        console.log('Oh shit! An error has ocorred.')
    });
}
