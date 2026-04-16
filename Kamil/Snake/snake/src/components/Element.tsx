import { useEffect, useRef } from "react";

const Element = (props : {x : number, y: number,team: string}) =>{
    const element = useRef<HTMLDivElement | null>(null);
    const team = props.team;

    
    useEffect(()=>{
        if(!props.x || !props.y || element.current === null) return 

        element.current.style.marginTop = `${props.y}px`;
        element.current.style.marginLeft = `${props.x}px`;
        
        if(team === 'r') element.current.className="rock";
        if(team === 's') element.current.className="scissors";
        if(team === 'p') element.current.className="paper";
    },[props]);

    
        
    return <div ref={element} ></div>


}

export default Element;