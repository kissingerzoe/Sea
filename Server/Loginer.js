//Loginer
var http=require('http');
var req_dic=new Array();
var user_dic;
var MongoClient = require('mongodb').MongoClient;
MongoClient.connect("mongodb://localhost:27017/SeaDB", function(err, db){
	if(err){
	}
	else{
	    console.log("DB connected..");
	    db.collection('user', function(err, collection){
		    if(!err){
			var myCursor=collection.find();
			myCursor.toArray(function(err,docs){
				user_dic=docs;
			    });
			http.createServer(function(req,res){
				console.log(req.url);
				if(req_dic[req.url]){
				    var body_info='';
				    req.on('data',function(chunk){
					    body_info+=chunk});
				    req.on('end',function(){
					    req_dic[req.url](arg_parse(body_info),res);
					});
				}else{
				    res.writeHead(202,{'Content-Type':'text/plain'});
				    res.end();
				}
			    }).listen(3000);
			console.log('Login is run at 3000');		    			
		    }
		});
	}
    })
    function arg_parse(_arg){
    console.log(_arg);
    var _o=new Object();
    var _args = _arg.split('&');
    for(var i=0;i<_args.length;++i){
	var _args1=_args[i].split('=');
	_o[_args1[0]]=_args1[1];
    }
    return _o;
}
//login
req_dic['/login']=function(arg,res){
    if(user_dic[0]){
	res.writeHead(200,{'Content-Type':'text/plain'});
	if((user_dic[0])&&(user_dic[0].name==arg.user)&&(user_dic[0].password==arg.pass)){
	    var _result=new Object();
	    _result.code=1;
	    _result.msg="LoginSuc";
	    res.end(JSON.stringify(_result));       
	}
	else{
	    var _result=new Object();
	    _result.code=0;
	    _result.msg="LoginFail";
	    res.end(JSON.stringify(_result));
	}
    }
    else{
	res.end('url error');	
    }
}
