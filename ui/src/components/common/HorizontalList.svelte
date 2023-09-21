<style>
	.list {
		display: flex;
		flex-wrap: wrap;
	}
	.list :global(.entry) {
		flex: auto;
		border: 1px solid #616161;
		margin: 0.2em;
		padding: 6px 12px;
		border-radius: 5px;
		background-color: #2a3037;
		color: #fff;
		font-size: 1.1rem;
		text-align: center;
		cursor: pointer;
	}
	.list :global(.entry:hover) {
		background-color: #21252b;
	}
	.list :global(.entry.active) {
		background-color: #104d0c;
	}
	.list :global(.entry.active:hover) {
		background-color: #135c0e;
	}
</style>

<div class="list">
	<slot />
</div>
