import { useEffect, useState } from "react";
import { Faktura } from "../Models/Faktura";
import { FaWiersz } from "../Models/FaWiersz";


const Table = (props: {body : Faktura})=>{
    const [headers,setHeaders] = useState<string[]>([]);
    const rows = props.body.FaWiersze;
    if(headers.length == 0)
    {
        const newHeaders = [];
        for(let header in props.body.FaWiersze[0])
            newHeaders.push(header);
        setHeaders(newHeaders);
    }


    return(
        <table>
            <tr>
                {headers.map((e)=> <th> {e} </th>)}
            </tr>
            {rows.map((e)=>
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