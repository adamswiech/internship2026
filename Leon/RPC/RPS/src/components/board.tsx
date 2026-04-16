import { useRef, useEffect } from "react";
import { createEntity, checkCollision, type RuntimeEntity } from "./entities";

export default function board() {
    const canvasRef = useRef<HTMLCanvasElement>(null);
    const eRef = useRef<RuntimeEntity[]>([]);
    const aRef = useRef<number>();

    useEffect(() => {
        const canvas = canvasRef.current;
        if (!canvas) return;

        const ctx = canvas.getContext("2d");
        if (!ctx) return;

        eRef.current = [
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
            createEntity({ x: 10, y: 10, color: "blue" }, "entity1"),
            createEntity({ x: 700, y: 50, color: "red", vx: -3, vy: 2 }, "entity2"),
            createEntity({ x: 10, y: 500, color: "green", vx: 4, vy: 3 }, "entity3"),
        ];

        const animate = () => {
            ctx.clearRect(0, 0, canvas.width, canvas.height);

            eRef.current.forEach(entity => {
                entity.update(canvas.width, canvas.height);
            });

            for (let i = 0; i < eRef.current.length; i++) {
                for (let j = i + 1; j < eRef.current.length; j++) {
                    if (checkCollision(eRef.current[i], eRef.current[j])) {
                        eRef.current[i].collide?.(eRef.current[j]);
                        eRef.current[j].collide?.(eRef.current[i]);
                    }
                }
            }

            eRef.current.forEach(entity => {
                entity.draw(ctx);
            });

            aRef.current = requestAnimationFrame(animate);
        };

        animate();

        return () => {
            if (aRef.current) {
                cancelAnimationFrame(aRef.current);
            }
        };
    }, []);

    return(
        <>
            <canvas ref={canvasRef} width={800} height={600} style={{border: '1px solid black'}}></canvas>
        </>
    )
}