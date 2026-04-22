import type { Top10 } from "../interfaces/interfaces";

const BASE_URL = "http://localhost:5179/api/leaderboard";

export async function addScore(params: {
username: string;
score: number;
time: string;
gameMode: string;
}): Promise<string> {
const url = `${BASE_URL}/addScore`;
const response = await fetch(url, {
    method: 'POST',
    headers: {
    'accept': 'text/plain',
    'Content-Type': 'application/json',
    },
    body: JSON.stringify(params),
});
if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
}
return response.text();
}
export async function getTop10(): Promise<Top10[]> {
const url = `${BASE_URL}/getTop10`;
const response = await fetch(url, {
    method: 'GET',
    headers: {
    'accept': 'application/json',
    'Content-Type': 'application/json',
    },
});
if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
}
return response.json();
}