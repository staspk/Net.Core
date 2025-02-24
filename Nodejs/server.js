const net = require('net');
const readline = require('readline');

const HOST = '127.0.0.1';
const PORT = 8080;
const clients = [];



const server = net.createServer((socket) => {
  clients.push(socket);
  socket.write('Welcome to the Node.js socket server!\n');
  
  socket.on('data', (data) => {
    console.log(`client: ${data}`);
    // Optionally, you can echo data back:
    // socket.write(`Echo: ${data}`);
  });
  
  socket.on('end', () => {
    console.log('Client disconnected');
    const index = clients.indexOf(socket);
    if (index !== -1) {
      clients.splice(index, 1);
    }
  });
  
  socket.on('error', (err) => {
    console.error(`Socket error: ${err}`);
  });
});

server.on('error', (err) => {
  console.error(`Server error: ${err}`);
});

server.listen(PORT, HOST, () => {
  console.log(`Server listening on ${HOST}:${PORT}`);
});

// Read from the console and broadcast to all clients
const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});


rl.on('line', (line) => {
  clients.forEach((socket) => {
    socket.write(`${line}`);
  });
});

function doWork() {
  let n = 10;
  for(let i = 0; i < n; i++) {
    console.log(`${i}`)
  }
}

doWork()
