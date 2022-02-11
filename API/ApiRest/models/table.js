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
    ocupada:{
        type: Boolean,
        required:false,
        default: false
    },
    id_zone:{
        type:String,
        required:true
    },
    comensales:{
        type:Number,
        required:false,
        default: 0
    },
    num_row:{
        type:Number,
        required:false
    },
    num_column:{
        type:Number,
        required:false
    },
    comensalesMax:{
        type:Number,
        required: true
    },
    id_user:{
        type:String,
        required:false,
        default: "Error"
    },
    id_ticket:{
        type: String,
        required: false,
        default: "Error"
    }   
})
module.exports = mongoose.model('Table', tableSchema);