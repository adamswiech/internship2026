import { useEffect, useState } from "react";
import type { FaWierszDTO } from "../Models/API/FaWierszDTO";
import type { FakturaDTO } from "../Models/API/FakturaDTO";


const Table = (props: {body : FakturaDTO})=>{
    const [headers,setHeaders] = useState<string[]>([]);
    const rows = props.body.faWiersze;
    const faktura = props.body;
    useEffect(()=>{
        const newHeaders = [];
        for(let header in props.body.faWiersze?.[0])
            newHeaders.push(header);
        setHeaders(newHeaders);
    },[props]);

    return(
        <table>
            <caption>{faktura.nrFaktury}</caption>
            <thead>
                <tr>
                    {headers.map((e)=> <th> {e} </th>)}
                </tr>
            </thead>

            <tbody>
                {rows?.map((e : any)=>
                    <tr>
                        {headers.map((h)=>
                            <td>
                                {e[h as keyof FaWierszDTO]}
                            </td>
                        )}
                    </tr>
                )}
            </tbody>
        </table>
    );
}

export default Table;