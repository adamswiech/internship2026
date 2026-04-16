export type Entity = {
  x: number;
  y: number;
  vx?: number;
  vy?: number;
  color?: string;
  h?: number;
  w?: number;
  mass?: number;
  force?: number;
  id?: string;
};
export type RuntimeEntity = {
  id: string;
  x: number;
  y: number;
  vx: number;
  vy: number;
  width: number;
  height: number;
  color: string;
  mass: number;
  force: number;
  draw(ctx: CanvasRenderingContext2D): void;
  update(canvasWidth: number, canvasHeight: number): void;
  collide(victim: RuntimeEntity): void;
};
export function createEntity(entity: Entity, id: string = Math.random().toString()) {
    return {
        id,
        x: entity.x,
        y: entity.y,
        vx: entity.vx ?? 5,
        vy: entity.vy ?? 5,
        width: entity.w ?? 6,
        height: entity.h ?? 6,
        color: entity.color ?? "blue",
        mass: entity.mass ?? 1,
        force: entity.force ?? 1,
        draw(ctx: CanvasRenderingContext2D) {
            ctx.fillStyle = this.color;
            ctx.fillRect(this.x, this.y, this.width, this.height);
        },
        update(cWidth: number, cHeight: number) {
            this.x += this.vx;
            this.y += this.vy;
            if (this.x + this.width >= cWidth || this.x <= 0) {
                this.vx = -this.vx;
                this.x = Math.max(0, Math.min(this.x, cWidth - this.width));
            }
            if (this.y + this.height >= cHeight || this.y <= 0) {
                this.vy = -this.vy;
                this.y = Math.max(0, Math.min(this.y, cHeight - this.height));
            }
        },
        collide(victim: RuntimeEntity){
            const aTeam = dictionary(this.color);
            const bTeam = dictionary(victim.color);
            if (!aTeam || !bTeam){
                return;
            }
            if( aTeam == bTeam){
                bounce(this, victim);
                return;
            }
            const res = aTeam - bTeam;
            if(res == -1 || res == 2){
                victim.color = this.color;
                bounce(this, victim);
                return;
            }
            this.color = victim.color;
            bounce(this, victim);
            return;
        }
    };
}

function rotation(vx: number, vy: number, angle: number) {
    const cos = Math.cos(angle);
    const sin = Math.sin(angle);
    return {
        x: vx * cos - vy * sin,
        y: vx * sin + vy * cos,
    };
}

function bounce(a: RuntimeEntity, b: RuntimeEntity) {
    const overlapX =
        Math.min(a.x + a.width - b.x, b.x + b.width - a.x);
    const overlapY =
        Math.min(a.y + a.height - b.y, b.y + b.height - a.y);
    const baseX = overlapX < overlapY ? (a.x < b.x ? -1 : 1) : 0;
    const baseY = overlapX < overlapY ? 0 : (a.y < b.y ? -1 : 1);
    const relVx = a.vx - b.vx;
    const relVy = a.vy - b.vy;
    const relSpeed = relVx * baseX + relVy * baseY;
    if (relSpeed > 0) {
        return;
    }

    const Force = (a.force + b.force) / 2;
    const i = -(1.0 + 0.9) * relSpeed * Force / (1 / a.mass + 1 / b.mass);
    const iX = i * baseX;
    const iY = i * baseY;

    a.vx += iX / a.mass;
    a.vy += iY / a.mass;
    b.vx -= iX / b.mass;
    b.vy -= iY / b.mass;

    const randomAng = (Math.random() - 0.5) * (Math.PI / 6);
    const aRot = rotation(a.vx, a.vy, randomAng);
    const bRot = rotation(b.vx, b.vy, -randomAng);
    a.vx = aRot.x;
    a.vy = aRot.y;
    b.vx = bRot.x;
    b.vy = bRot.y;
    if (overlapX < overlapY) {
        if (a.x < b.x) {
            a.x -= overlapX;
        } else {
            a.x += overlapX;
        }
    } else {
        if (a.y < b.y) {
            a.y -= overlapY;
        } else {
            a.y += overlapY;
        }
    }
}


const dictionary = (a: string) => {
    switch(a){
        case "red":
            return 1;
        case "green":
            return 2;
        case "blue":
            return 3;
        default:
            return;
    }
}
export function checkCollision(a: RuntimeEntity, b: RuntimeEntity): boolean {
  return (a.x + a.width >= b.x && a.x < b.x + b.width && a.y + a.height >= b.y && a.y < b.y + b.height);
}
