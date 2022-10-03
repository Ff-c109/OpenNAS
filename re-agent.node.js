const https = require("https");
const http = require("http");
const fs = require("fs");

var options = {
    key: fs.readFileSync("./server.private.key"),
    cert: fs.readFileSync("./server.crt")
};
var server = https.createServer(options);

server.listen(fs.readFileSync("sslPort").toString().split('\n')[0]);
server.on('request', (request, response) => {
    var roptions = {
        hostname: fs.readFileSync("host").toString().split('\n')[0],
        port: parseInt(fs.readFileSync("httpPort")).toString().split('\n')[0],
        path: request.url,
        method: 'GET'
    };

    //console.log(fs.readFileSync("host").toString().split('\n')[0]);
    console.log(request.url);

    var req = http.request(roptions, (res) => {
        res.on('data', data => {
            response.write(data, 'utf-8', () => {
                response.end();
            });
        });
    });
    req.on('error', error => {
        console.log(error);
    });
    req.end();
});