const mongoose = require("mongoose");

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