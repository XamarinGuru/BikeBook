'use strict';

const mongoose = require('mongoose');

const Schema = mongoose.Schema;

const userSchema = mongoose.Schema({ 

	name 			    : String,
    email               : String, 
    description         : String,
    styleSport          : Boolean,
    styleTouring        : Boolean,
    styleCruising       : Boolean,
    styleAdventure      : Boolean,
    styleTrack          : Boolean,
    styleCommuting      : Boolean,
    picture             : String,
	hashed_password	    : String,
	created_at		    : String,
	temp_password	    : String,
	temp_password_time  : String
	
});

mongoose.Promise = global.Promise;

module.exports = mongoose.model('user', userSchema);        