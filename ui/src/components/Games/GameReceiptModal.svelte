<style>
  h2 {
    font-size: 2em;
    text-align: center;
    margin-bottom: 12px;
  }
</style>

<script lang="ts">
  import { type BasicGameInfoDto, ReceiptDto, GameReceiptClient } from "../../nlp-api";
	import GameReceiptAdder from "./GameReceiptAdder.svelte";
	import GameReceiptEditor from "./GameReceiptEditor.svelte";

  export let game: BasicGameInfoDto;
  let loading = true;
  let receipt: ReceiptDto | undefined;

  const refreshReceiptInfo = (_game: BasicGameInfoDto | undefined) => {
    if(!_game) return;
    loading = true;
    receipt = undefined;

    new GameReceiptClient().getOrderInformation(_game.receiptID)
      .then((_receiptInfo: ReceiptDto) => {
        if(!_receiptInfo) return;
        receipt = _receiptInfo;
      })
      .finally(() => loading = false);
  };

  const receiptAdded = (addedReceipt: ReceiptDto) => {
    receipt = addedReceipt;
  };

  $: refreshReceiptInfo(game);
</script>

{#if !game}
  Please select a game first.
{:else}
    {#if loading}
      Loading...
    {:else}
      <h2>{game.gameName}</h2>
      {#if receipt}
        <GameReceiptEditor {receipt} />
      {:else}
        <GameReceiptAdder {game} onReceiptAdded={receiptAdded} />
      {/if}
    {/if}
{/if}
