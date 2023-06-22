<style>
  .card {
    border: 1px solid #2d2d2d;
    margin: 6px;
    border-radius: 6px;
    background-color: #161616;
    box-shadow: 0px 0px 5px 1px rgb(95 91 91);
    flex: 1 0;
    width: 150px;
    max-width: 150px;
  }
  h2 { text-align: center; font-size: 1.1em; padding: 6px 2px 6px 2px; background-color: #023b00; border-bottom: 1px solid #777; }
  .card.sold h2 { background-color: #830000; }
  .image { position: relative; text-align: center; }
  img.cover { width: 150px; border-radius: 5px; }
  .sold { opacity: 0.25; }
  .price { background-color: rgba(0,0,0,.61); border-radius: 3px; color: #00ff1f; padding: 3px; position: absolute; right: 5px; top: 5px; }
</style>

<script lang="ts">
  import type { BasicGameInfoDto } from "../../nlp-api";

  export let game: BasicGameInfoDto;
  export let triggerAction: (action: string, game: BasicGameInfoDto) => void;
</script>

<div class="card" class:sold={game.gameSold}>
  <h2 class="ellipsis">{game.gameName}</h2>
  <div class="image">
    {#if game.gamePrice > 0}<div class="price">{game.gamePrice}</div>{/if}
    <!-- svelte-ignore a11y-missing-attribute -->
    <img class="cover" src={`api/images/game/cover/${game.platformName}/${game.gameID}`} />
  </div>
  <div class="buttons">
    {#if game.receiptID > 0}<button on:click={() => triggerAction('receipt', game)}>Receipt</button>{/if}
  </div>
</div>