<style>
    .console {
        padding: 2px 4px;
        cursor: pointer;
        display: flex;
        margin-top: 6px;
    }
    .console:hover {
        color: rgb(37, 241, 81);
    }
    .console i {
        font-size: 24px;
    }
    .console span {
        font-size: 1.2em;
        text-align: right;
        flex: auto;
    }
</style>

<script lang="ts">
	import type { BasicGameInfoDto } from "../../nlp-api";

    export let onClick: () => void;

    export let game: BasicGameInfoDto;
</script>

{#if !game.gameSold}
    <!-- svelte-ignore a11y-click-events-have-key-events -->
    <div class="console" on:click={onClick}>
        <i class="bi bi-controller"></i>
        <span>{game.locationName}</span>
    </div>    
{/if}