import type {PersonalDataModel} from "../src/interfaces/PersonalDataModel";

export default class Api {
public static async fetchData(offset : number, limit : number, ): Promise<PersonalDataModel[]> {
        const response = await fetch(`https://localhost:7503/api/Data/fetchData?offset=${offset}&limit=${limit}`);
        const jsonResponse: PersonalDataModel[] = await response.json();

        if (!response.ok) {
            throw new Error("HTTP error! status: " + response.status);
        }

        return jsonResponse;
        }public static async filterData(offset : number, limit : number, firstName : string, lastName : string, ): Promise<PersonalDataModel[]> {
        const response = await fetch(`https://localhost:7503/api/Data/filterData?offset=${offset}&limit=${limit}&firstName=${firstName}&lastName=${lastName}`);
        const jsonResponse: PersonalDataModel[] = await response.json();

        if (!response.ok) {
            throw new Error("HTTP error! status: " + response.status);
        }

        return jsonResponse;
        }}
