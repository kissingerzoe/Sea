//Game
var http=require('http');
var req_dic=new Array();
var MongoClient = require('mongodb').MongoClient;
var m_db;
var m_user_collection;
MongoClient.connect("mongodb://localhost:27017/SeaDB", function(err, db){
	if(err){console.error(err);}else{
	    m_db=db;console.log("Game DB connected..");
	    m_db.collection('user',function(err, collection){
		    if(!err){
			m_user_collection=collection;
			var port=3001;
			if(process.argv.length>2){port=process.argv[2];}
			http.createServer(function(req,res){
				res.writeHead(200,{'Content-Type':'text/plain'});
				console.log(req.url);
				if(req_dic[req.url]){
				    var body_info='';
				    req.on('data',function(chunk){body_info+=chunk});
				    req.on('end',function(){ req_dic[req.url](arg_parse(body_info),res)});
				}else{
				    var _result=new Object();
				    _result.code=-1;
				    _result.msg="UrlError";
				    res.end(JSON.stringify(_result));
				}
			    }).listen(port);
			console.log('Game is run at port:'+port);
		    }
		});
	}
    });

function arg_parse(_arg){
    _arg=decodeURIComponent(_arg);
    console.log(_arg);
    var _o=new Object();
    var _args = _arg.split('&');
    for(var i=0;i<_args.length;++i){
	//console.log(_args[i]);
	var _args1=_args[i].split('=');
	_o[_args1[0]]=_args1[1];
    }
    return _o;
}

//login
req_dic['/enter']=function(arg,res){
    m_user_collection.findOne({name:arg.user,password:arg.pass},function(err,item){
	    if(err){
		console.log(err);
	    }else{
		if(item){
		    console.log(item.name+"  "+item.password);
		    var _result=new Object();
		    _result.code=1;
		    _result.msg="login ok";
		    _result.wuid=item._id;;
		    res.end(JSON.stringify(_result));
		    console.log("welecome "+arg.user);
		    return;
		}
	    }
	    var _result=new Object();
	    _result.code=0;
	    _result.msg="user or pass error";
	    res.end(JSON.stringify(_result));
	} );
}
var BSON = require('mongodb').BSONPure;
//save ship data
req_dic['/save_ship_data']=function(arg,res){
    m_user_collection.findOne({_id: BSON.ObjectID.createFromHexString(arg.wuid)},function(err,item){
	    if(err){
		console.log(err);
	    }else{
		if(item){
		    //console.log(item.name+"  "+item.password);
		    var _result=new Object();
		    _result.code=1;
		    _result.msg="ok";
		    res.end(JSON.stringify(_result));
		    m_user_collection.update({ _id: BSON.ObjectID.createFromHexString(arg.wuid)},{$set:{ship_data: JSON.parse(arg.data)}},function(err,ooo){
			    if(!err){
				 console.log("save data ok ");
			    }
			    else{
				console.log("save data error");
			    }
			}); 
		    return;
		}
	    }
	    var _result=new Object();
	    _result.code=0;
	    _result.msg="ship save fail";
	    res.end(JSON.stringify(_result));
	});
}
//get ship data
req_dic['/get_ship_data']=function(arg,res){
    m_user_collection.findOne({_id: BSON.ObjectID.createFromHexString(arg.wuid)},function(err,item){
	    if(err){
		console.log(err);
	    }else{
		if(item){
		    //console.log(item.name+"  "+item.password);
		    var _result=new Object();
		    _result.code=1;
		    _result.msg="ok";
		    _result.ship_data=item.ship_data;
		    res.end(JSON.stringify(_result));
		    return;
		}
	    }
	    var _result=new Object();
	    _result.code=0;
	    _result.msg="ship get fail";
	    res.end(JSON.stringify(_result));
	});
}

