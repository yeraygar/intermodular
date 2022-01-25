const mongoose = require("mongoose");

/*
    -> name 
    -> cantidad
    -> precio
    -> id_client
    -> id_familia => creamos una familia "all" al que pertenezcan todos
        los productos, un producto puede pertenecer a varias familias simultaneamente.
*/

const productSchema = mongoose.Schema({
    name:{
        type:String,
        required:true
    },
    cantidad:{
        type: Number,
        required:false,
        default: 1
    },
    precio:{
        type: Number,
        required: false,
        default: 0.0
    },
    id_client:{
        type: String,
        required: true
    },
    id_familia:{
        type:Array,
        required:false,
        default: ["all"]
    }

})
module.exports = mongoose.model('Product', productSchema);