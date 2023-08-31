<style>
  .card { font-size: 0.85em;}
  .sold { opacity: 0.25; }
  .price { background-color: rgba(0,0,0,.61); border-radius: 3px; color: #00ff1f; padding: 3px; position: absolute; right: 5px; top: 5px; }
  .flex-spaced span { flex: auto; padding: 0 4px; }
  .game-id { color: #0e701a; }
  .location { text-align: center; }
  .rating { text-align: right; color: #1b7bb3; }
  .flags { padding: 4px; text-align: center; margin: 2px 6px 6px 6px; }
  .flags i { margin-right: 4px; }
  .flags i:last-child { margin-right: 0; }
  .receipt span:last-child { text-align: right; }
  i.receipt { cursor: pointer; }
  i.receipt.has { color: #379aeb; }
  .receipt .none { font-style: italic; }
  .cover, .card-title { cursor: pointer; }
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

<div class="col">
	<div class="card h-100" class:sold={game.gameSold}>
    <!-- svelte-ignore a11y-click-events-have-key-events -->
    <img class="cover" src={`/api/images/game/cover/${game.platformName}/${game.gameID}`} alt="game cover" on:click={onGameClicked} />
    {#if game.gamePrice > 0}<div class="price">{game.gamePrice}</div>{/if}
		<!-- <div class="title">Title</div> -->
		<div class="card-body mb-0 p-1">
      <!-- svelte-ignore a11y-click-events-have-key-events -->
      <h5 class="card-title text-truncate" on:click={onGameClicked}>{game.gameName}</h5>
      <div class="d-flex flex-spaced">
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
      <div class="receipt d-flex flex-spaced">
        {#if game.hasReceipt}
          <span class="text-truncate">{receiptName}</span>
          <span class="text-truncate">{receiptNumber}</span>
        {:else}
          <span class="none">No receipt information</span>
        {/if}
      </div>
      <GameCardCurrentConsole {game} onClick={onConsoleClicked} />
    </div>
  </div>
</div>