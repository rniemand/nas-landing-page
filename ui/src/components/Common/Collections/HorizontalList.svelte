<style>
	.links-container {
		display: flex;
		flex-wrap: wrap;
		justify-content: space-evenly;
		width: 100%;
	}
	.links-container :global(.link) {
		flex: auto;
		background-color: #2f2f2f;
		border: 1px solid #979797;
		border-radius: 8px;
		padding: 3px;
		color: white;
		margin-right: 2px;
		cursor: pointer;
		min-width: 100px;
		text-align: center;
	}
	.links-container :global(.link:last-child) {
		margin-right: 0;
	}
	.links-container :global(.link:hover) {
		background-color: #222222;
	}
	.links-container :global(.link.active) {
		background-color: rgb(38, 80, 25);
	}
</style>

<div class="mt-2">
	<div class="links-container">
		<slot />
	</div>
</div>
