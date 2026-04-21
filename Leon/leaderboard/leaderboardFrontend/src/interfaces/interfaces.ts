
export interface Score {
    id: number;
    username: string;
    score: number;
    time: string;
    gameMode: string;
    isSuspicious: boolean | null;
    player: player | null;
}


export interface player {
    id: number;
    username: string;
    scoreQ: number;
    avgScore: number;
    highScore: number;
    scores: Score[];
}
export interface Top10 {
    id: number;
    rank: number | null;
    scoreId: number | null;
    score: Score | null;
}

export interface Top10snapshot {
    id: number;
    date: string;
    entries: snapshotEntry[];
}

export interface snapshotEntry {
    id: number;
    rank: number;
    username: string;
    score: number;
    time: string;
    gameMode: string;
    isSuspicious: boolean;
    top10snapshotId: number;
}