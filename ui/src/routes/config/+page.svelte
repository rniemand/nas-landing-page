<style>
	.items {
		display: flex;
		flex-wrap: wrap;
		justify-content: center;
	}
	.item {
		border: 1px solid #6f6f6f;
		width: 200px;
		display: block;
		padding: 1.5em 0.2em;
		border-radius: 0.5em;
		text-align: center;
		cursor: pointer;
		font-size: 1.2em;
		background-color: #292e32;
		margin: 0.2em;
		text-decoration: none;
		color: #fff;
	}
	.item:hover {
		background-color: #373e44;
	}
	.item i.bi {
		display: block;
		font-size: 3em;
	}
</style>

<script lang="ts">
	import { Col, Row } from 'sveltestrap';
	import { ConfigUrls } from '../../enums/AppUrls';
</script>

<Row>
	<Col>
		<h1 class="text-center">Configuration</h1>
		<div class="items mt-3">
			<a class="item" href={ConfigUrls.Floors}>
				<i class="bi bi-building-fill-up" />
				Floors
			</a>
			<a class="item" href={ConfigUrls.Rooms}>
				<i class="bi bi-sort-numeric-up" />
				Rooms
			</a>
		</div>
	</Col>
</Row>
