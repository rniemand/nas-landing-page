<style>
  .card {
    border: 1px solid #2d2d2d;
    margin: 6px;
    border-radius: 6px;
    background-color: #161616;
    box-shadow: 0px 0px 5px 1px rgb(95 91 91);
    /* flex: 1 0; */
    width: 150px;
    max-width: 150px;
  }
  h2 { text-align: center; font-size: 1.1em; padding: 6px 2px 6px 2px; background-color: #023b00; border-bottom: 1px solid #777; }
  .card.sold h2 { background-color: #830000; }
  .image { position: relative; text-align: center; margin-top: 4px; margin-bottom: 4px; }
  img.cover { border-radius: 5px; max-height: 200px; width: auto; max-width: 140px; }
  .sold { opacity: 0.25; }
  .price { background-color: rgba(0,0,0,.61); border-radius: 3px; color: #00ff1f; padding: 3px; position: absolute; right: 5px; top: 5px; }
  .flags { padding: 4px; text-align: center; background-color: #4747476e; border-radius: 7px; margin: 2px 10px 6px 10px; }
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
</style>

<script lang="ts">
  import type { BasicGameInfoDto } from "../../nlp-api";
  export let game: BasicGameInfoDto;
  export let triggerAction: (action: string, game: BasicGameInfoDto) => void;

  const onReceiptClicked = () => {
    if(!game.haveReceipt && game.gameSold) return;
    triggerAction('receipt', game)
  };
</script>

<div class="card" class:sold={game.gameSold}>
  <h2 class="ellipsis">{game.gameName}</h2>
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
      <i class="bi bi-receipt receipt" class:has={game.haveReceipt} title="Has Receipt" on:click={onReceiptClicked}></i>
      {#if game.receiptScanned}<i class="bi bi-printer" title="Receipt Scanned"></i>{/if}
    </div>
    {#if game.haveReceipt}
      <div class="receipt flex-spaced">
        <span>{game?.receiptName || '-'}</span>
        <span>{game?.receiptNumber || '-'}</span>
      </div>
    {/if}
    
    <span>{game.locationName}</span>
    
    <span>{game.store}</span>
  </div>
</div>