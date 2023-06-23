<style>
  h2 {
    font-size: 2em;
    text-align: center;
    margin-bottom: 12px;
  }
</style>

<script lang="ts">
  import { ReceiptClient, type BasicGameInfoDto, ReceiptDto } from "../../nlp-api";
	import GameReceiptAdder from "./GameReceiptAdder.svelte";
	import GameReceiptEditor from "./GameReceiptEditor.svelte";

  export let game: BasicGameInfoDto;
  let loading = true;
  let receipt: ReceiptDto | undefined;

  const refreshReceiptInfo = (_game: BasicGameInfoDto | undefined) => {
    if(!_game) return;
    loading = true;
    receipt = undefined;

    new ReceiptClient().getOrderInformation(_game.receiptID)
      .then((_receiptInfo: ReceiptDto) => {
        if(!_receiptInfo) return;
        receipt = _receiptInfo;
      })
      .finally(() => loading = false);
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
        <GameReceiptAdder />
      {/if}
    {/if}
{/if}
