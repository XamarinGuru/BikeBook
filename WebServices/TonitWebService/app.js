'use strict';

const express    = require('express');        
const app        = express();                
const bodyParser = require('body-parser');
const logger 	   = require('morgan');
const router 	   = express.Router();
const port = process.env.PORT || 8080;
const mongoose = require('mongoose');

app.use(bodyParser.json());
app.use(logger('dev'));

require('./routes')(router);
mongoose.connect('mongodb://192.168.0.11:27017/Tonit');
app.use('/api/v1', router);

app.listen(port);

console.log(`Tonit Runs on ${port}`);