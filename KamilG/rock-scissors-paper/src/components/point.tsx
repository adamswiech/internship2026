import {useEffect, useRef } from "react";


const Point = (props: { x: number, y: number, A: number }) => {
    const point = useRef<HTMLDivElement | null>(null);



useEffect(() => {
    if (point.current) {
        point.current.style.setProperty('margin-left', props.x + 'px');
        point.current.style.setProperty('margin-top', props.y + 'px');
    }
}, [props]);


return <div ref={point}></div>
}

export default Point;