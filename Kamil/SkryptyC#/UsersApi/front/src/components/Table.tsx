import { useEffect, useState } from "react";
import type { User } from '../Models/User';

const Table = (props: {body : User[]})=>{
    const [headers,setHeaders] = useState<string[]>([]);
    const rows = props.body;
    useEffect(()=>{
        const newHeaders = [];
        for(let header in props.body?.[0])
            newHeaders.push(header);
        setHeaders(newHeaders);
    },[props]);
    if(rows.length === 0) return <p>No data</p>

    return(
        <table>
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
                                {(typeof e[h] == 'boolean')? (e[h])?'Tak': 'Nie':e[h] }
                            </td>
                        )}
                    </tr>
                )}
            </tbody>
        </table>
    );
}

export default Table;