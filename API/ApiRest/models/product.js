const mongoose = require("mongoose");

const productSchema = mongoose.Schema({
    name:{
        type:String,
        required:true
    },
    cantidad:{
        type: Number,
        required:false,
        default: 0
    },
    precio:{
        type: Number,
        required: false,
        default: 0.0
    },
    total:{
        type: Number,
        required: false,
        default: 0.0 // cantidad * precio
    },
    id_client:{
        type: String,
        required: true
    },
    id_familia:{
        type:String,
        required: true,
    },
    id_ticket:{
        type:String,
        required: false
    }

})
module.exports = mongoose.model('Product', productSchema);