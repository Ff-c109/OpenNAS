var fs = require('fs');
var https = require('https');
var httpProxy = require('http-proxy');

var proxy = httpProxy.createProxyServer();

proxy.on('error', function(err, req, res) {
    res.end();
});

var options = {
    key: fs.readFileSync("./server.private.key"),
    cert: fs.readFileSync("./server.crt")
};

var proxy_server = https.createServer(options);

proxy_server.listen(fs.readFileSync("./sslPort").toString().split('\n')[0], function() {
    console.log('proxy server is running ');
});

proxy_server.on('request', (req, res) => {
    proxy.web(req, res, {
        target: fs.readFileSync("targetURL").toString().split('\n')[0]
    });
});