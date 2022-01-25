const mongoose = require("mongoose");

/*
    -> name
    -> status (ocupada o libre)
    -> numero_mesa (posicion en el grid, valor auto)
    -> id_cliente (empresa, no haria falta porque la zona ya lo lleva)
    -> id_zona (_id de zona en la que va, cambiar cuenta de la mesa con una mesa de otra zona?)
    -> comensales (no requerido, default 1)
    -> cuenta (deberia ser una especie de hashMap de
        nombre_producto : array con valores tipo: cantidad, id_producto, precio)
*/

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