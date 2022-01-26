const mongoose = require("mongoose");

const tableSchema = mongoose.Schema({
    name:{
        type:String,
        required:true
    },
    status:{
        type:Boolean,
        required:false,
        default: true
    },
    id_zone:{
        type:String,
        required:true
    },
    numero_mesa:{
        type:Number,
        required:true
    },
    comensales:{
        type:Number,
        required:false
    },
    id_user:{
        type:String,
        required:false
    }
    //falta cuenta [array de productos]
   
})
module.exports = mongoose.model('Table', tableSchema);