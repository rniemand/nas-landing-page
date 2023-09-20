<script lang="ts">
	export let active: boolean = false;
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<div class="link {$$props.class}" class:active on:click>
	<slot />
</div>
