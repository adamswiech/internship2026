interface Entity {
  role: string;
  //   x: number,
  //   y: number
  entityRef: any;
}

export default function Entity({ role, entityRef }: Entity) {
  return (
    <div
      className="entity"
      id="entity"
      //   style={{marginTop: y, marginLeft: x}}
      ref={entityRef}
    >
      {role}
    </div>
  );
}
