import Entity from "./components/entity/Entity";

function App() {
  return (
    <>
      <div className="app-container">
        <h2>rock-scissors-paper simulation</h2>
        <div className="game-board">
          <Entity role="🪨"/>
          {/* <Entity role="✂️"/>
          <Entity role="📃"/> */}
        </div>
      </div>
    </>
  );
}

export default App;
