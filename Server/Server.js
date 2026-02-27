const express = require("express");

const cors = require("cors");

const app = express();
const PORT = 3000;

app.use(cors());
app.use(express.json());

let scores = [];

app.post("/score",(req,res)=>{
const{player,score} = req.body;

console.log("Puntuacion recibida:");
console.log("Jugador: ",player);
console.log("Score: ",score);

scores.push({player,score});

res.json({
status: "OK",
message:"Puntuacion guardada correctamente",
totalScores: scores.length
});
});

app.get("/scores",(req,res) => {
res.json(scores);
});

app.listen(PORT, "0.0.0.0", () => {
console.log(`Servidor corriendo en http://localhost:${PORT}`);
});
