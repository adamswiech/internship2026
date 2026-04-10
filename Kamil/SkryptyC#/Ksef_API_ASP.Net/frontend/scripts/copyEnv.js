import { exec } from  'node:child_process';

// Run a bash script
exec('sed "s/^/REACT_APP_/" ../.env > .env', (error, stdout, stderr) => {
  if (error) {
    console.error(`Error: ${error.message}`);
    return;
  }
  if (stderr) {
    console.error(`Stderr: ${stderr}`);
    return;
  }
  console.log(`Output:\n${stdout}`);
});