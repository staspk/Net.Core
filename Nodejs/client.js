const net = require('net');
const readline = require('readline');

const HOST = '127.0.0.1';
const PORT = 8080;


const client = net.createConnection(PORT, HOST, () => {
  const data = Buffer.from([0xDA, 0xAD]);
  // const data = Buffer.from('DAAD', 'hex');
  client.write(data);
  client.end();
});


client.on('data', (data) => {
  console.log(`Server: ${data}`);
});

client.on('end', () => {
  process.exit()
});

client.on('error', (err) => {
  console.error(`Client error: ${err}`);
});

// Read from the console and send to the server
const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

rl.on('line', (line) => {
  client.write(line);
});