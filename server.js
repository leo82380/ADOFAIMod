const express = require('express');
const cors = require('cors');
const app = express();
const fs = require('fs');
let port = 5000;

app.use(cors());

app.get('/test.txt', function(req, res){
    res.sendFile(__dirname + '/test.txt');
})

app.listen(port, () => {
    console.log("wa sans");
})