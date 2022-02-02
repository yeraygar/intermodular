const express = require("express");
const router = express.Router();
const ticketLineSchema = require("../models/product");


//Create new ticket_line
router.post("/ticket_line", (req, res) => {
    const ticket_line = ticketLineSchema(req.body);
    ticket_line
        .save()
        .then((data) =>{
            res.json(data);
            console.log(`\nNew ticket_line: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.error(`Error get /api/ticket_line : ${err}`);
        })
})

//Get specific ticket_line
router.get("/ticket_line/:id", (req, res) => {
    const {id} = req.params;
    ticketLineSchema
        .findById(id)
        .then((data) =>{
            res.json(data);
            console.log(`\nticket_line: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/ticket_line/${id} : ${err}`);
        })
})

//Update ticket_line, si algun campo no se pone no se elimina
router.put("/ticket_line/:id", (req, res) => {
    const {id} = req.params;
    const {name, email, active} = req.body;
    ticketLineSchema
        .updateOne({_id: id}, {$set:{name, email, active}})
        .then((data) =>{
            res.json(data);
            console.log(`\nticket_line Update succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`ticket_line Error update : ${err}`);
        })
})

//Delete ticket_line
router.delete("/ticket_line/:id", (req, res) => { 
    const {id} = req.params;
    ticketLineSchema
        .remove({_id: id})
        .then((data) =>{
            res.json(data);
            console.log(`\nticket_line Delete succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`ticket_line Error delete : ${err}`);
        })
})

//Get all ticket_line from a client
router.get("/ticket_line/client/:id_client", (req, res) => {
    const {id_client} = req.params;
    ticketLineSchema
        .find({id_client: id_client})
        .then((data) =>{
            res.json(data);
            console.log(`\nticket_line: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/ticket_line/client/${id} : ${err}`);
        })
})

//Get all ticket_line from a family
router.get("/ticket_line/family/:id_familia", (req, res) => {
    const {id_familia} = req.params;
    ticketLineSchema
        .find({id_familia: id_familia})
        .then((data) =>{
            res.json(data);
            console.log(`\nticket_line: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/ticket_line/client/${id} : ${err}`);
        })
})

module.exports = router