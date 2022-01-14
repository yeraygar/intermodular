const express = require("express");
const router = express.Router();
const zoneSchema = require("../models/zone");

//Create new Zone
router.post("/zones", (req, res) => {
    const zone = zoneSchema(req.body);
    zone
        .save()
        .then((data) =>{
            res.json(data);
            console.log(`\nNew Zone: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.error(`Error get /api/zones : ${err}`);
        })
})

//Get all Zones
router.get("/zones", (req, res) => {
    zoneSchema
        .find()
        .then((data) =>{
            res.json(data);
            console.log(`\nAll Zones: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/zones : ${err}`);
        })
})

//Get specific Zone
router.get("/zones/:id", (req, res) => {
    const {id} = req.params;
    zoneSchema
        .findById(id)
        .then((data) =>{
            res.json(data);
            console.log(`\nZone: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/zones/${id} : ${err}`);
        })
})

//Update Zone, si algun campo no se pone no se elimina
router.put("/zones/:id", (req, res) => {
    const {id} = req.params;
    const {num_tables, zone_status} = req.body;
    zoneSchema
        .updateOne({_id: id}, {$set:{num_tables, zone_status}})
        .then((data) =>{
            res.json(data);
            console.log(`\nUpdate succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error update : ${err}`);
        })
})

//Delete Zone
router.delete("/zones/:id", (req, res) => {
    const {id} = req.params;
    zoneSchema
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

module.exports = router;