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
  // BLUE (12)
  createEntity({ x: 10, y: 10, color: "blue", mass: 1, force: 0.2 }, "blue_0"),
  createEntity({ x: 10, y: 10, color: "blue", mass: 2, force: 0.4 }, "blue_1"),
  createEntity({ x: 10, y: 10, color: "blue", mass: 3, force: 0.6 }, "blue_2"),
  createEntity({ x: 10, y: 10, color: "blue", mass: 4, force: 0.8 }, "blue_3"),
  createEntity({ x: 10, y: 10, color: "blue", mass: 5, force: 1.0 }, "blue_4"),
  createEntity({ x: 10, y: 10, color: "blue", mass: 6, force: 1.2 }, "blue_5"),
  createEntity({ x: 10, y: 10, color: "blue", mass: 1, force: 1.4 }, "blue_6"),
  createEntity({ x: 10, y: 10, color: "blue", mass: 2, force: 1.6 }, "blue_7"),
  createEntity({ x: 10, y: 10, color: "blue", mass: 3, force: 1.8 }, "blue_8"),
  createEntity({ x: 10, y: 10, color: "blue", mass: 4, force: 2.0 }, "blue_9"),
  createEntity({ x: 10, y: 10, color: "blue", mass: 5, force: 0.5 }, "blue_10"),
  createEntity({ x: 10, y: 10, color: "blue", mass: 6, force: 0.9 }, "blue_11"),

  // RED (12)
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 1, force: 0.3 }, "red_0"),
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 2, force: 0.6 }, "red_1"),
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 3, force: 0.9 }, "red_2"),
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 4, force: 1.2 }, "red_3"),
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 5, force: 1.5 }, "red_4"),
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 6, force: 1.8 }, "red_5"),
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 1, force: 2.0 }, "red_6"),
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 2, force: 0.4 }, "red_7"),
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 3, force: 0.7 }, "red_8"),
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 4, force: 1.0 }, "red_9"),
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 5, force: 1.3 }, "red_10"),
  createEntity({ x: 790, y: 10, color: "red", vx: -3, vy: 2, mass: 6, force: 1.6 }, "red_11"),

  // GREEN (12)
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 1, force: 0.2 }, "green_0"),
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 2, force: 0.5 }, "green_1"),
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 3, force: 0.8 }, "green_2"),
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 4, force: 1.1 }, "green_3"),
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 5, force: 1.4 }, "green_4"),
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 6, force: 1.7 }, "green_5"),
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 1, force: 2.0 }, "green_6"),
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 2, force: 0.3 }, "green_7"),
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 3, force: 0.6 }, "green_8"),
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 4, force: 0.9 }, "green_9"),
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 5, force: 1.2 }, "green_10"),
  createEntity({ x: 10, y: 590, color: "green", vx: 4, vy: -3, mass: 6, force: 1.5 }, "green_11"),
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
            <canvas ref={canvasRef} width={1000} height={1000} style={{border: '1px solid black'}}></canvas>
        </>
    )
}