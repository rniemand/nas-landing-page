<script lang="ts">
	export let active: boolean = false;
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<li class="page-item" on:click>
	<a class="page-link" class:active href="#!">
		<slot />
	</a>
</li>
