const express = require("express");
const router = express.Router();
const tableSchema = require("../models/table");

//Create new Table
router.post("/tables", (req, res) => {
    const table = tableSchema(req.body);
    table
        .save()
        .then((data) =>{
            res.json(data);
            console.log(`\nNew Table: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.error(`Error get /api/table : ${err}`);
        })
})

//Get all Tables
router.get("/tables", (req, res) => {
    tableSchema
        .find()
        .then((data) =>{
            res.json(data);
            console.log(`\nAll Tables: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/tables : ${err}`);
        })
})

//Get specific table
router.get("/tables/:id", (req, res) => {
    const {id} = req.params;
    tableSchema
        .findById(id)
        .then((data) =>{
            res.json(data);
            console.log(`\nTable: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/tables/${id} : ${err}`);
        })
})

//Update table, si algun campo no se pone no se elimina
router.put("/tables/:id", (req, res) => {
    const {id} = req.params;
    const {name, status, id_client, id_zone, id_row, id_column, comensales, id_user} = req.body;
    tableSchema
        .updateOne({_id: id}, {$set:{name, status, id_client, id_zone, id_row, id_column, comensales, id_user}})
        .then((data) =>{
            res.json(data);
            console.log(`\nUpdate succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error update : ${err}`);
        })
})

//Delete table
router.delete("/tables/:id", (req, res) => {
    const {id} = req.params;
    tableSchema
        .remove({_id: id})
        .then((data) =>{
            res.json(data);
            console.log(`\nDelete succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error delete : ${err}`);
        })
})

//Get all tables from a zone
router.get("/tables/zone/:id_zone", (req, res) => {
    const {id_zone} = req.params;
    tableSchema
        .find({id_zone: id_zone})
        .then((data) =>{
            res.json(data);
            console.log(`\nACTIVES: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/users/clien`);
        })
})

//Delete all tables from a zone
router.delete("/tables/zone/:id_zone" , (req,res) => {
    const {id_zone} = req.params;
    tableSchema
    .deleteMany({id_zone: id_zone})
    .then((data) => {
        res.json(data);
        console.log(`\nDelete succesful: \n ${data}`);
    })
    .catch((err) => {
        res.json(data);
        console.log(`Error delete : ${err}`);
    })
})
module.exports = router;