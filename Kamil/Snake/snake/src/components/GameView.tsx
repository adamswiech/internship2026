import { useRef,useState,useEffect } from "react";
import Element from "./Element";
const GameView = ()=>{

    const [time, setTime] = useState(Date.now());
    const [parentBorder,setParentBorder] = useState({bot:0,right:0});
    const parent = useRef<HTMLDivElement | null>(null);
    const elementList = useRef([{x: 0,y: 0,alfa: (1/10) * Math.PI,team:'r'}]);
    const r = 2;

    useEffect(()=>{
        parent.current.style.height = "1000px";
        parent.current.style.width = "1000px";
        let tmpList = [];
        let num = 100
        let radnomStart = 1000;
        for(let i = 0 ; i < num; i++)
            tmpList.push({x:0 + Math.random() * radnomStart,y:0 + Math.random() * radnomStart,alfa:(1/6) * Math.PI * (Math.random()),team: 'r'})


        for(let i = 0 ; i < num; i++)
            tmpList.push({x:990 - (Math.random() * radnomStart),y:990  - (Math.random() * radnomStart),alfa:(14/11) * Math.PI * (Math.random()% 0.2 + 0.8),team: 's'})


        for(let i = 0 ; i < num; i++)
            tmpList.push({x:0 + Math.random() * radnomStart,y:990 - Math.random() * radnomStart,alfa:(4/3) * Math.PI * (Math.random()% 0.2 + 0.8),team: 'p'})
        
        // for(let i = 0 ; i < num; i++)
        // {
        //     let randomN = Math.random();
        //     let team = '';
        //     if(randomN > 0.66)
        //         team = 'r'
        //     else if(randomN > 0.33)
        //         team = 'p'
        //     else
        //         team = 's'
        //     tmpList.push({x:990 - Math.random()* 10 * radnomStart,y: 10 + Math.random() * 10 * radnomStart,alfa:(4/3) * Math.PI * (Math.random()% 0.2 + 0.8),team: team})
        // }

        elementList.current = tmpList;
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
         elementList.current = elementList.current.map((element) =>{
            
            let top = r * Math.cos(element.alfa)  + element.y;
            if(top + 5 >= parentBorder.bot) {
                element.alfa += (Math.PI / 3) * (Math.random()% 0.2 + 0.8);
            }
            if(top <= 0){
                element.alfa -= (Math.PI / 3) * (Math.random()% 0.2 + 0.8);
            }
            element.y = top;
            
            let left = r * Math.sin(element.alfa) + element.x;
            if(left + 10 >= parentBorder.right){
                element.alfa += (Math.PI / 2) * (Math.random()% 0.2 + 0.8);
            }
            if(left <= 0) element.alfa -= (Math.PI / 2) * (Math.random()% 0.2 + 0.8);
            element.x = left;

            const u = element.y ;
            const d = element.y + 10;
            const l = element.x ;
            const re = element.x + 10; 
            elementList.current.forEach((e)=>{
                if(((l < e.x && e.x < re) || (l < e.x + 10 && e.x + 10 < re)) && ((u < e.y && e.y<d) ||(u < e.y + 10 && e.y + 10<d)))
                {
                    const checkBorder = (n) => (n > 10 && n + 10< 990);
                    const checkNearBorder = (n) => (n > 100 && n + 10< 950);
                    const repelForce = 0.2;
                    if(checkBorder(e.x) && checkBorder(element.x) && Math.random() > 0.8)
                    {
                        if(e.x > element.x)
                            element.x +=(e.x - element.x) * repelForce;
                        else if(e.x < element.x)
                            element.x -=(e.x - element.x) * repelForce;
        
                    }
                    if(checkBorder(e.y) && checkBorder(element.y) && Math.random() > 0.8)
                    {
                        if(e.y > element.y)
                            element.y +=(e.y - element.y) * repelForce;
                        else if(e.y < element.y)
                            element.y -=(e.y - element.y) * repelForce;
                    }
                    // debugger;
                    if(checkNearBorder(element.x) && checkNearBorder(element.y)) 
                        element.alfa =  Math.random() * Math.PI;
        
                    if(e.team !== element.team) 
                    {
                        // debugger
                        let team = checkWinner(e.team,element.team);
                        
                        element.team = team;
                        let index = elementList.current.findIndex((a) => a.x === e.x);
                        elementList.current[index].team = team;
                    }
                }
            }
        );
            return(element);
        });
        
        return () => clearInterval(interval);
        
    }, [time]);
    
    // console.log(elementList);
    return(
        <div ref={parent} className="Border" >
            {elementList.current.map((e)=>{ return<Element x={e.x} y={e.y}  team={e.team}/>})}
        </div>
    );
}

export default GameView;