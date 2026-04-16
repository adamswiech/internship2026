import { useRef,useState,useEffect } from "react";
import Element from "./Element";
const GameView = ()=>{

    const [time, setTime] = useState(Date.now());
    const [parentBorder,setParentBorder] = useState({bot:0,right:0});
    const parent = useRef<HTMLDivElement | null>(null);
    const [elementList,setElementList] = useState([{x: 0,y: 0,alfa: (1/10) * Math.PI,team:'r'}]);
    const r = 2;

    useEffect(()=>{
        parent.current.style.height = "1000px";
        parent.current.style.width = "1000px";
        let tmpList = [];
        let num = 6
        for(let i = 0 ; i < num; i++)
            tmpList.push({x:0,y:0,alfa:(1/6) * Math.PI * (Math.random()),team: 'r'})


        for(let i = 0 ; i < num; i++)
            tmpList.push({x:990,y:990,alfa:(4/3) * Math.PI * (Math.random()% 0.2 + 0.8),team: 's'})


        for(let i = 0 ; i < num; i++)
            tmpList.push({x:0,y:990,alfa:(4/3) * Math.PI * (Math.random()% 0.2 + 0.8),team: 'p'})
        
        setElementList(tmpList);
    },[]);

    useEffect(()=>{
        if(!parent.current) return;

        setParentBorder( {
            bot: - -(parent.current.style.height.substring(0, parent.current.style.height.length - 2)),
            right: - -(parent.current.style.width.substring(0, parent.current.style.width.length - 2))
        });

    },[parent]);

    const checkWinner = (a :string, b:string) =>{
        // debugger;
        const d = {
            'r': 2,
            'p': 3,
            's': 5
        }
        let machInNumber = d[a] * d[b];
        if(machInNumber % 2 === 0 && machInNumber % 3 === 0) return'p';
        if(machInNumber % 2 === 0 && machInNumber % 5 === 0) return'r';
        if(machInNumber % 5 === 0 && machInNumber % 3 === 0) return's';

    };

    useEffect(() => {
        const interval = setInterval(() => setTime(Date.now()), 10);
        if(parentBorder.bot === 0) return;
        setElementList(elementList.map((element) =>{
            const u = element.y ;
            const d = element.y + 10;
            const l = element.x ;
            const re = element.x + 10; 
            elementList.forEach((e)=>{
                if(((l <= e.x && e.x <= re) || (l <= e.x + 10 && e.x + 10 <= re)) && ((u <= e.y && e.y<=d) ||(u <= e.y + 10 && e.y + 10<=d)))
                {
                    if(e.x > element.x)
                        element.x +=e.x - element.x;
                    else
                        element.x -=e.x - element.x;

                    if(e.y > element.y)
                        element.y +=e.y - element.y;
                    else
                        element.y -=e.y - element.y;

                    if(e.team !== element.team) 
                    {
                        // debugger
                        let team = checkWinner(e.team,element.team);
                        
                        element.team = team;
                        let index = elementList.findIndex((a) => a.x === e.x);
                        elementList[index].team = team;
                    }
                }
            }
            )
            
            let top = r * Math.cos(element.alfa)  + element.y;
            if(top + 5 >= parentBorder.bot) element.alfa += (Math.PI / 2) * (Math.random()% 0.2 + 0.8);
            if(top <= 0) element.alfa -= (Math.PI / 2) * (Math.random()% 0.2 + 0.8);
            element.y = top;
            
            let left = r * Math.sin(element.alfa) + element.x;
            if(left + 5 >= parentBorder.right) element.alfa += (Math.PI / 2) * (Math.random()% 0.2 + 0.8);
            if(left <= 0) element.alfa -= (Math.PI / 2) * (Math.random()% 0.2 + 0.8);
            element.x = left;

                return(element);
            }));
        
        return () => clearInterval(interval);
        
    }, [time]);

    // console.log(elementList);
    return(
        <div ref={parent} className="Border" >
            {elementList.map((e)=>{ return<Element x={e.x} y={e.y}  team={e.team}/>})}
        </div>
    );
}

export default GameView;