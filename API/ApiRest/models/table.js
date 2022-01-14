const mongoose = require("mongoose");
const tableSchema = mongoose.Schema({
    name:{
        type:String,
        required:true
    },
    status:{
        type:Boolean,
        required:true
    },
    id_client:{
        type:String,
        required: true
    },
    id_zone:{
        type:Number,
        required:true
    },
    id_row:{
        type:Number,
        required:true
    },
    id_column:{
        type:Number,
        required:true
    },
    comensales:{
        type:Number,
        required:false
    },
    id_user:{
        type:String,
        required:true
    }
   
})
module.exports = mongoose.model('Table', tableSchema);