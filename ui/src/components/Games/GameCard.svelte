<style>
  .card {
    margin-right: 6px;
    margin-bottom: 12px;
    width: 50%;
    min-width: 100px;
    max-width: 150px;
    cursor: pointer;
  }
  .card-body { padding: 6px; }
  .card figure img { max-height: 220px; width: 100%; }
  .flags { padding: 4px; text-align: center; margin: 2px; }
  .flags i { margin-right: 4px; }
  .flags i:last-child { margin-right: 0; }
  .receipt { display: flex; justify-content: space-evenly; flex-wrap: wrap; font-size: 0.85em; }
  .receipt span { flex: 1; text-align: center; }
  .receipt .none { font-style: italic; }
  .flex-spaced { display: flex; }
  .flex-spaced span { flex: auto; padding: 0 4px; }
  .game-id { color: #0e701a; }
  .location { text-align: center; }
  .rating { text-align: right; color: #1b7bb3; }
  .sold { opacity: 0.6; }
  .price { background-color: rgba(0,0,0,.61); border-radius: 3px; color: #00ff1f; padding: 3px; position: absolute; right: 5px; top: 5px; }
</style>

<script lang="ts">
  import type { BasicGameInfoDto } from "../../nlp-api";
	import GameCardCurrentConsole from "./GameCardCurrentConsole.svelte";
	import { GameModal } from "./Games";
  export let game: BasicGameInfoDto;
  export let triggerAction: (action: string, game: BasicGameInfoDto) => void;
  let receiptNumber = '-';
  let receiptName = '-';

  const setGameInfo = (_game: BasicGameInfoDto) => {
    receiptName = (_game?.receiptName?.length || 0) === 0 ? '-' : _game.receiptName;
    receiptNumber = (_game?.receiptNumber?.length || 0) == 0 ? '-' : _game.receiptNumber;
  };

  const onReceiptClicked = () => {
    if(!game.hasReceipt && game.gameSold) return;
    triggerAction(GameModal.Receipt, game);
  };

  const onGameClicked = () => {
    if(!game.hasReceipt && game.gameSold) return;
    triggerAction(GameModal.GameInfo, game);
  };

  const onConsoleClicked = () => {
    if(!game.hasReceipt && game.gameSold) return;
    triggerAction(GameModal.SetConsole, game);
  };

  $: setGameInfo(game);
</script>

<div class="card card-compact bg-base-300 shadow-xl" class:sold={game.gameSold}>
  <!-- svelte-ignore a11y-click-events-have-key-events -->
  <figure on:click={onGameClicked}>
    <img src={`/api/images/game/cover/${game.platformName}/${game.gameID}`} alt={game.gameName} />
    {#if game.gamePrice > 0}
      <div class="price">{game.gamePrice}</div>
    {/if}
  </figure>
  <div class="card-body p-2">
    <div class="truncate game-name">{game.gameName}</div>
    <div class="flex-spaced">
      <span class="game-id">#{game.gameID}</span>
      <span class="location">{game?.gameCaseLocation || '-'}</span>
      <span class="rating">{game?.gameRating || 0}/10</span>
    </div>
    <div class="flags">
      {#if game.hasGameBox}<i class="bi bi-box2-fill" title="Has Box"></i>{/if}
      {#if game.hasProtection}<i class="bi bi-currency-dollar" title="Has Protection"></i>{/if}
      <a href="#!" on:click={onReceiptClicked}>
        <i class="bi bi-receipt" class:has={game.hasReceipt} title="Has Receipt"></i>
      </a>
      {#if game.receiptScanned}<i class="bi bi-printer" title="Receipt Scanned"></i>{/if}
    </div>
    <div class="receipt">
      {#if game.hasReceipt}
        <span class="truncate">{receiptName}</span>
        <span class="truncate">{receiptNumber}</span>
      {:else}
        <span class="none">No receipt information</span>
      {/if}
    </div>
    <GameCardCurrentConsole {game} onClick={onConsoleClicked} />
  </div>
</div>