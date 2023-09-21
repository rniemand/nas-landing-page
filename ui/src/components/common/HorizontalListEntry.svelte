<script lang="ts">
	export let active: boolean = false;
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<div class="entry" on:click class:active>
	<slot />
</div>
