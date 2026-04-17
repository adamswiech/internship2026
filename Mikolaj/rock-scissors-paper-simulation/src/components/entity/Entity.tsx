interface Entity {
  role: string;
  entityRef: any;
}

export default function Entity({ role, entityRef }: Entity) {
  return (
    <div
      className="entity"
      id="entity"
      ref={entityRef}
    >
      {role}
    </div>
  );
}
