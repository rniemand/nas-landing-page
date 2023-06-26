<style>
  .card {
    border: 1px solid #2d2d2d;
    margin: 6px;
    border-radius: 6px;
    background-color: #161616;
    box-shadow: 0px 0px 5px 1px rgb(95 91 91);
    /* flex: 1 0; */
    width: 50%;
    max-width: 180px;
  }
  h2 { text-align: center; font-size: 1.1em; padding: 6px 2px 6px 2px; background-color: #023b00; border-bottom: 1px solid #777; }
  .card.sold h2 { background-color: #830000; }
  .image { position: relative; text-align: center; margin-top: 4px; margin-bottom: 4px; }
  img.cover { border-radius: 5px; max-height: 200px; width: auto; max-width: 160px; }
  .sold { opacity: 0.25; }
  .price { background-color: rgba(0,0,0,.61); border-radius: 3px; color: #00ff1f; padding: 3px; position: absolute; right: 5px; top: 5px; }
  .flags { padding: 4px; text-align: center; background-color: #4747476e; border-radius: 7px; margin: 2px 6px 6px 6px; }
  .flex-spaced { display: flex; }
  .flex-spaced span { flex: auto; padding: 0 4px; }
  .game-id { color: #00ff1f; }
  .location { text-align: center; }
  .rating { text-align: right; color: #1b7bb3; }
  .receipt span:last-child { text-align: right; }
  .flags i { margin-right: 4px; }
  .flags i:last-child { margin-right: 0; }
  i.receipt { cursor: pointer; }
  i.receipt.has { color: #00ff1f; }
  h2.game-name { cursor: pointer; }
  .receipt .none { font-style: italic; }
</style>

<script lang="ts">
  import type { BasicGameInfoDto } from "../../nlp-api";
	import GameCardCurrentConsole from "./GameCardCurrentConsole.svelte";
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
    triggerAction('receipt', game);
  };

  const onGameClicked = () => {
    if(!game.hasReceipt && game.gameSold) return;
    triggerAction('game-info', game);
  };

  const onConsoleClicked = () => {
    if(!game.hasReceipt && game.gameSold) return;
    triggerAction('set-console', game);
  };

  $: setGameInfo(game);
</script>

<div class="card" class:sold={game.gameSold}>
  <!-- svelte-ignore a11y-click-events-have-key-events -->
  <h2 class="ellipsis game-name" on:click={onGameClicked}>{game.gameName}</h2>
  <div class="image">
    {#if game.gamePrice > 0}<div class="price">{game.gamePrice}</div>{/if}
    <!-- svelte-ignore a11y-missing-attribute -->
    <img class="cover" src={`api/images/game/cover/${game.platformName}/${game.gameID}`} />
  </div>
  <div class="attributes">
    <div class="flex-spaced">
      <span class="game-id">#{game.gameID}</span>
      <span class="location">{game?.gameCaseLocation || '-'}</span>
      <span class="rating">{game?.gameRating || 0}/10</span>
    </div>
    <div class="flags">
      {#if game.hasGameBox}<i class="bi bi-box2-fill" title="Has Box"></i>{/if}
      {#if game.hasProtection}<i class="bi bi-currency-dollar" title="Has Protection"></i>{/if}
      <!-- svelte-ignore a11y-click-events-have-key-events -->
      <i class="bi bi-receipt receipt" class:has={game.hasReceipt} title="Has Receipt" on:click={onReceiptClicked}></i>
      {#if game.receiptScanned}<i class="bi bi-printer" title="Receipt Scanned"></i>{/if}
    </div>
    <div class="receipt flex-spaced">
      {#if game.hasReceipt}
          <span class="ellipsis">{receiptName}</span>
          <span class="ellipsis">{receiptNumber}</span>
        {:else}
          <span class="none">No receipt information</span>
      {/if}
    </div>
    <GameCardCurrentConsole {game} onClick={onConsoleClicked} />
  </div>
</div>