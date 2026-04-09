import fs from 'node:fs/promises';

export async function loadJSON(filename){
    try {
        const data = await fs.readFile(filename, 'utf8');
        return JSON.parse(data);
    } catch (error) {
        console.error('Error reading JSON file:', error.message);
        throw error;
    }
}