<style>
  h2 {
    font-size: 2em;
    text-align: center;
    margin-bottom: 12px;
  }
</style>

<script lang="ts">
  import { type BasicGameInfoDto, GameReceiptDto, GameReceiptClient } from "../../nlp-api";
	import GameReceiptAdder from "./GameReceiptAdder.svelte";
	import GameReceiptEditor from "./GameReceiptEditor.svelte";

  export let game: BasicGameInfoDto;
  let loading = true;
  let receipt: GameReceiptDto | undefined;

  const refreshReceiptInfo = (_game: BasicGameInfoDto | undefined) => {
    if(!_game) return;
    loading = true;
    receipt = undefined;

    new GameReceiptClient().getOrderInformation(_game.receiptID)
      .then((_receiptInfo: GameReceiptDto) => {
        if(!_receiptInfo) return;
        receipt = _receiptInfo;
      })
      .finally(() => loading = false);
  };

  const updateReceipt = (addedReceipt: GameReceiptDto) => receipt = addedReceipt;

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
        <GameReceiptEditor {receipt} onReceiptChanged={updateReceipt} />
      {:else}
        <GameReceiptAdder {game} onReceiptAdded={updateReceipt} />
      {/if}
    {/if}
{/if}
