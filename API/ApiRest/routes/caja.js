const express = require("express");
const router = express.Router();
const cajaSchema = require("../models/caja");

//Create new caja
router.post("/caja", (req, res) => {
    const caja = cajaSchema(req.body);
    caja
        .save()
        .then((data) =>{
            res.json(data);
            console.log(`\nNew caja: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.error(`Error get /api/caja : ${err}`);
        })
})

//Get specific caja
router.get("/caja/:id", (req, res) => {
    const {id} = req.params;
    cajaSchema
        .findById(id)
        .then((data) =>{
            res.json(data);
            console.log(`\ncaja: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/caja/${id} : ${err}`);
        })
})

//Update caja, si algun campo no se pone no se elimina
router.put("/caja/:id", (req, res) => {
    const {id} = req.params;
    const {fecha_apertura, fecha_cierre, total, cerrada} = req.body;
    cajaSchema
        .updateOne({_id: id}, {$set:{fecha_apertura, fecha_cierre, total, cerrada}})
        .then((data) =>{
            res.json(data);
            console.log(`\ncaja Update succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`caja Error update : ${err}`);
        })
})

//Delete caja
router.delete("/caja/:id", (req, res) => { 
    const {id} = req.params;
    cajaSchema
        .remove({_id: id})
        .then((data) =>{
            res.json(data);
            console.log(`\ncaja Delete succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`caja Error delete : ${err}`);
        })
})

//Get all cajas from a client
router.get("/caja/client/:id_client", (req, res) => {
    const {id_client} = req.params;
    cajaSchema
        .find({id_client: id_client})
        .then((data) =>{
            res.json(data);
            console.log(`\ncaja: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/caja/client/${id} : ${err}`);
        })
})

//Comprueba si el cliente tiene alguna caja abierta
router.get("/caja/client/:id_client/open", (req, res) => {
    const {id_client} = req.params;
    cajaSchema
        .find({id_client: id_client, cerrada: false})
        .then((data) =>{
            res.json(data);
            console.log(`\ncaja hasOpencaja: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/caja/client/${id} : ${err}/open`);
        })
})

module.exports = router