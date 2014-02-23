//Loginer
var http=require('http');
//var mongodb=require('mongodb');
var req_dic=new Array();
req_dic['/login']=function()
{
    // console.log('want login?');
}
var MongoClient = require('mongodb').MongoClient;

// Connect to the db
MongoClient.connect("mongodb://localhost:27017/exampleDb", function(err, db) {
	if(!err) {
	    console.log("We are connected");
	    db.collection('test', function(err, collection) {});

	    db.collection('test', {w:1}, function(err, collection) {});

	    db.createCollection('test', function(err, collection) {});

	    db.createCollection('test', {w:1}, function(err, collection) {});
	}
    })



http.createServer(function(req,res)		  
{
    console.log(req.url);
    if(req_dic[req.url])
    {
       req_dic[req.url]();
       res.writeHead(200,{'Content-Type':'text/plain'});
       var body_info='';
       req.on('data',function(chunk){body_info+=chunk});
       req.on('end',function(){console.log('data  '+body_info);res.end();});
    }else
    {
	res.writeHead(202,{'Content-Type':'text/text'});
	res.end();
    }
}).listen(3000);
console.log('Login is run at 3000');
