import { useEffect, useState } from "react";
import { Faktura } from "../Models/API/Faktura";
import { FaWiersz } from "../Models/API/FaWiersz";


const Table = (props: {body : Faktura})=>{
    const [headers,setHeaders] = useState<string[]>([]);
    const rows = props.body.faWiersze;
    debugger;
    if(headers.length == 0)
    {
        const newHeaders = [];
        for(let header in props.body.faWiersze[0])
            newHeaders.push(header);
        setHeaders(newHeaders);
    }

    return(
        <table>
            <tr>
                {headers.map((e)=> <th> {e} </th>)}
            </tr>
            {rows.map((e : any)=>
                <tr>
                    {headers.map((h)=>
                        <td>
                            {e[h as keyof FaWiersz]}
                        </td>
                    )}
                </tr>
            )}
        </table>
        );
}

export default Table;