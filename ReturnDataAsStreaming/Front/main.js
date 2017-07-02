console.log('Hello mothafucker!');

const params = {
    url: 'http://localhost/webapi/api/home/search/a',
    method: 'GET'
};

oboe(params).node('!', (json) => {
    console.log(json);
}).node('eof', () =>{
    console.log('Finish');
}).fail(() => {
    console.log('Oh shit! An error has ocorred.')
});